function FormatPrice(price) {
    return price.toLocaleString('en-US', {
        style: 'currency',
        currency: 'USD',
    });
}

function OnMarketData(message) {

    const tradableUpdatePayload = JSON.parse(message);
    const tradablePriceInfos = tradableUpdatePayload["TradablePriceInfos"];
    const symbol = tradableUpdatePayload["Symbol"];
    const price = tradablePriceInfos["Price"];

    const priceLabelsOfSymbol = document.getElementsByClassName("TradablePrice_" + symbol);

    for (let priceLabel of priceLabelsOfSymbol) {
        priceLabel.textContent = FormatPrice(price);
    }
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