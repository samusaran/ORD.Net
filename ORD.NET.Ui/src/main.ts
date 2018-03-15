import { app, BrowserWindow } from 'electron'

declare var __dirname: string
let mainWindow: Electron.BrowserWindow

function onReady() {
    mainWindow = new BrowserWindow({
        width: 800,
        height: 600,
        minWidth: 700,
        minHeight: 500,
        frame: false
    })

    const fileName = `file://${__dirname}/index.html`
    mainWindow.loadURL(fileName)
    mainWindow.on('close', () => app.quit())
    mainWindow.webContents.openDevTools({ mode: 'detach' });

    require('devtron').install();
}

app.on('ready', () => onReady())
app.on('window-all-closed', () => app.quit())
console.log(`Electron Version ${app.getVersion()}`)
