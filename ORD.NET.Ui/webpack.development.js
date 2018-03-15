'use strict'

const common = require('./webpack.config')

const merge = require('webpack-merge')
//const webpack = require('webpack')

const mainConfig = merge({}, common, {
    target: "electron-main",
    entry: { main: './src/main.ts' }
})

const rendererConfig = merge({}, common, {
    target: "electron-renderer",
    entry: { index: './src/index.tsx' },
    plugins: [
    ]
})

module.exports = [
    mainConfig,
    rendererConfig
]