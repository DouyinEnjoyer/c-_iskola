const { contextBridge, ipcRenderer } = require('electron');


contextBridge.exposeInMainWorld('api', {
    getPlatform: () => ipcRenderer.invoke('get-platform'),
    openSecondWindow: () => ipcRenderer.invoke('open-second-window'),
});
