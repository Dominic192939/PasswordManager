// content.js
console.log("PasswordManager content script running");


const passwordFields = document.querySelectorAll('input[type="password"]');

passwordFields.forEach(field => {

    console.log("Password field found on page:", field);

    chrome.runtime.sendMessage({ type: "passwordFieldDetected" });
});
