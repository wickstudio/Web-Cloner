

var apiUrl = "http://localhost:6990";
var onBeforeRequest_callback = function(details) {
    if(details != null && !details.url.startsWith(apiUrl) ){
        
        var data = JSON.stringify(details);
        console.log(data);
        console.log("Project by Wick Studio");
        console.log("discord.gg/wicks");
        var req = new XMLHttpRequest();
        req.onreadystatechange = function(){
            if(req.status == 200 && req.readyState == 4){
        
            }
        }
        req.open("post", apiUrl);
        req.send(data);  
    }

    return {};
}

var isEnable = false;
var contextMenuItem = {
    "id": "EnableExt",
    "title": "Enable Bullshit",
    "contexts": ["page"]
};

chrome.contextMenus.create(contextMenuItem);
chrome.contextMenus.onClicked.addListener(function (info, tab) {
    if (info.menuItemId == "EnableExt" && !isEnable) {
        isEnable = true;
        chrome.webRequest.onBeforeRequest.addListener(
            onBeforeRequest_callback,
            {urls: ["<all_urls>"]},
            ["blocking", "requestBody"]
        );
        alert("Started Listen");
    }
});