'use strict'

const path = require('path')
const HtmlWebpackPlugin = require('html-webpack-plugin')
const merge = require('webpack-merge')
const webpack = require('webpack')

const replacements = {
    __API_URL__: JSON.stringify("alosi.sisteminet.it:5000")
}

const commonConfig = {
    output: {
        path: path.resolve(__dirname, 'dist'),
        filename: '[name].js'
    },
    module: {
        rules: [
            {
                test: /\.js$/,
                enforce: 'pre',
                exclude: /node_modules/,
                loader: 'standard-loader'
            },
            {
                test: /\.jsx?$/,
                exclude: /node_modules/,
                loader: 'babel-loader'
            },
            {
                test: /\.ts$/,
                enforce: 'pre',
                loader: 'tslint-loader',
                options: {
                    typeCheck: true,
                    emitErrors: true
                }
            },
            {
                test: /\.tsx?$/,
                loader: ['babel-loader', 'ts-loader']
            },
            {
                test: /\.css$/,
                use: ['style-loader', 'css-loader']
            },
            {
                test: /\.(?:png|jpg|svg|gif)$/,
                loader: 'url-loader'
            }
        ]
    },
    node: {
        __dirname: false
    },
    resolve: {
        //root: [ path.resolve('./src')],
        extensions: ['.js', '.ts', '.tsx', '.jsx', '.json']
    }
}

const mainConfig = merge({}, commonConfig, {
    target: "electron-main",
    entry: { main: './src/main.ts' }
})

const rendererConfig = merge({}, commonConfig, {
    target: "electron-renderer",
    entry: { index: './src/index.tsx' },
    plugins: [
        new HtmlWebpackPlugin({
            title: 'ORD.Net UI',
            template: './src/index.html'
        }),
        new webpack.DefinePlugin(replacements)
    ]
})

module.exports = [
    mainConfig,
    rendererConfig
]