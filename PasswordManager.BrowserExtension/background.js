chrome.runtime.onMessage.addListener((message, sender, sendResponse) => {
    if (message.type === "openPopupWindow") {
        chrome.tabs.query({ active: true, currentWindow: true }, function (tabs) {
            const tabUrl = encodeURIComponent(tabs[0].url);
            chrome.windows.create({
                url: chrome.runtime.getURL("popup.html") + "?url=" + tabUrl,
                type: "popup",
                width: 400,
                height: 340
            });
        });
    }
});
