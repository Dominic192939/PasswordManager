console.log("PasswordManager content script running");

function showMiniPopup(target) {
    if (document.getElementById("pm-mini-popup")) return;

    const popup = document.createElement("div");
    popup.id = "pm-mini-popup";
    popup.innerText = "💾 Save to PasswordManager";

    const rect = target.getBoundingClientRect();
    popup.style.position = "absolute";
    popup.style.top = `${rect.top + window.scrollY + 30}px`;
    popup.style.left = `${rect.left + window.scrollX}px`;
    popup.style.backgroundColor = "#007acc";
    popup.style.color = "white";
    popup.style.padding = "6px 12px";
    popup.style.borderRadius = "5px";
    popup.style.zIndex = "9999";
    popup.style.cursor = "pointer";
    popup.style.fontSize = "12px";
    popup.style.boxShadow = "0 2px 6px rgba(0,0,0,0.2)";
    popup.style.transition = "opacity 0.2s ease-in-out";

    popup.onclick = () => {
        try {
            chrome.runtime.sendMessage({ type: "openPopupWindow" });
        } catch (e) {
            console.warn("Context invalidated when sending message:", e);
        }
        popup.remove();
    };

    document.body.appendChild(popup);

    setTimeout(() => {
        popup.remove();
    }, 5000);
}


function attachPopupToPasswordFields() {
    const passwordFields = document.querySelectorAll('input[type="password"]');

    passwordFields.forEach(field => {
        if (!field.dataset.pmAttached) {
            field.dataset.pmAttached = "true";
            console.log("Password field found:", field);

            field.addEventListener('focus', () => {
                showMiniPopup(field);
            });
        }
    });
}

attachPopupToPasswordFields();

const observer = new MutationObserver(() => {
    attachPopupToPasswordFields();
});

observer.observe(document.body, { childList: true, subtree: true });
