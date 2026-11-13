const { app, BrowserWindow, ipcMain } = require('electron');
const path = require('path');

function createWindow(file) {
    const win = new BrowserWindow({
        width: 600,
        height: 400,
        webPreferences: {
            preload: path.join(__dirname, 'preload.js'),
        },
    });
    win.loadFile(file);
    return win;
}


app.whenReady().then(() => {
    ipcMain.handle('get-platform', () => {
        return process.platform;
    });
    ipcMain.handle('open-second-window', () => {
        createWindow('second.html');
    });
    createWindow("index.html");
});
