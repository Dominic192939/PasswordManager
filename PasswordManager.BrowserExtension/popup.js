document.addEventListener('DOMContentLoaded', () => {
    const params = new URLSearchParams(window.location.search);
    const siteParam = params.get('url');

    const siteInput = document.getElementById('site');

    if (siteParam) {
        try {
            const domain = new URL(siteParam).hostname.replace(/^www\./, '');
            if (siteInput && domain) {
                siteInput.value = domain;
            }
        } catch (err) {
            console.error("Invalid URL param:", err);
        }
    } else {
        chrome.tabs.query({ active: true, currentWindow: true }, function (tabs) {
            try {
                const url = tabs[0].url;
                const domain = new URL(url).hostname.replace(/^www\./, '');
                if (siteInput && domain) {
                    siteInput.value = domain;
                }
            } catch (e) {
                console.warn("Konnte aktuelle Tab-URL nicht lesen:", e);
            }
        });
    }

    const passwordInput = document.getElementById('password');
    const togglePassword = document.getElementById('togglePassword');

    if (passwordInput && togglePassword) {
        togglePassword.addEventListener('click', () => {
            const isPassword = passwordInput.type === 'password';
            passwordInput.type = isPassword ? 'text' : 'password';
            togglePassword.textContent = isPassword ? '🙈' : '👁️';
        });
    }
    const passwordForm = document.getElementById('passwordForm');
    const statusText = document.getElementById('status');

    if (passwordForm && statusText) {
        passwordForm.addEventListener('submit', function (event) {
            event.preventDefault(); 
            statusText.textContent = "✔️ Eintrag gespeichert!";
            statusText.style.color = "green";


            setTimeout(() => {
                statusText.textContent = "";
            }, 3000);
        });
    }

});
