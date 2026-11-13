const nameInput = document.getElementById('nameInput');
const greetButton = document.getElementById('greetButton');
const messageLabel = document.getElementById('messageLabel');

function greet() {
    const name = nameInput.value.trim();

    if (name === '') {
        messageLabel.textContent = 'Előbb írd be a neved!';
    } else {
        messageLabel.textContent = `Szia, ${name}!`;
    }
}

greetButton.addEventListener('click', greet);

const platformButton = document.getElementById('platformButton');
const platformLabel = document.getElementById('platformLabel');

async function showPlatform() {
    const platform = await window.api.getPlatform();
    platformLabel.textContent = `Operációs rendszer: ${platform}`;
}

platformButton.addEventListener('click', showPlatform);

const openWindowButton = document.getElementById('openWindowButton');

openWindowButton.addEventListener('click', () => {
    window.api.openSecondWindow();
});
