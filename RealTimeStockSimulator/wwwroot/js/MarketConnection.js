
function OnMarketData(message) {

    const tradableUpdatePayload = JSON.parse(message);
    const tradablePriceInfos = tradableUpdatePayload["TradablePriceInfos"];
    const symbol = tradableUpdatePayload["Symbol"];
    const price = tradablePriceInfos["Price"];

    console.log(symbol);
    console.log(price);
}

function init() {

    CurrentConnection = new signalR.HubConnectionBuilder().withUrl("/marketHub").build();

    CurrentConnection.start();
    CurrentConnection.on("ReceiveMarketData", OnMarketData);
    window.addEventListener("beforeunload", () => {
        CurrentSocket.close();
    });
}

init();