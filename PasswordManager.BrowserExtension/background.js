// background.js
chrome.runtime.onMessage.addListener((message, sender, sendResponse) => {
    if (message.type === "passwordFieldDetected") {
        console.log("Password field detected on site:", sender.tab.url);
        
    }
});
