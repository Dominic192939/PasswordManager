console.log("Popup loaded.");

document.addEventListener('DOMContentLoaded', async () => {
    let [tab] = await chrome.tabs.query({ active: true, currentWindow: true });

    try {
        const url = new URL(tab.url);
        const domain = url.hostname;
        document.getElementById('site').value = domain;
    } catch (err) {
        console.error("Invalid URL", err);
    }

    const passwordInput = document.getElementById('password');
    const togglePassword = document.getElementById('togglePassword');

    togglePassword.addEventListener('click', () => {
        const isPassword = passwordInput.type === 'password';
        passwordInput.type = isPassword ? 'text' : 'password';
        togglePassword.textContent = isPassword ? '🙈' : '👁️';
    });
});
