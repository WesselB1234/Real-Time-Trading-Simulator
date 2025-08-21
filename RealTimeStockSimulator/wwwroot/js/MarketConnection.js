function FormatPrice(price) {
    return price.toLocaleString('en-US', {
        style: 'currency',
        currency: 'USD',
    });
}

function IsNumber(value) {
    return !isNaN(parseFloat(value)) && isFinite(value);
}

function UpdatePriceLabel(priceLabel, symbol, price) {

    if (IsNumber(priceLabel.id) != false) {

        const amountLabel = document.getElementById("TradableAmount_" + priceLabel.id + "_" + symbol);
        const amount = parseInt(amountLabel.textContent);

        price *= amount;
    } 

    priceLabel.textContent = FormatPrice(price);
}

function OnMarketData(message) {

    const tradableUpdatePayload = JSON.parse(message);
    const tradablePriceInfos = tradableUpdatePayload["TradablePriceInfos"];
    const symbol = tradableUpdatePayload["Symbol"];
    const price = tradablePriceInfos["Price"];

    const priceLabelsOfSymbol = document.getElementsByClassName("TradablePrice_" + symbol);

    for (let priceLabel of priceLabelsOfSymbol) {
        UpdatePriceLabel(priceLabel, symbol, price);
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