function isNumber(value) {
    return !isNaN(parseFloat(value)) && isFinite(value);
}

function updatePriceLabels(updatedSymbol, newPrice) {

    const priceLabels = document.querySelectorAll(
        `[data-price-symbol="${updatedSymbol}"]`
    );

    for (const priceLabel of priceLabels) {

        if (isNumber(priceLabel.dataset.amountLabelNumber) != false) {

            const amountLabel = document.querySelector(
                `[data-amount-label-symbol="${updatedSymbol}"][data-amount-label-number="${priceLabel.dataset.amountLabelNumber}"]`
            );

            const amount = parseInt(amountLabel.dataset.amountLabelValue);
            newPrice *= amount;
        }

        let currentPrice = priceLabel.dataset.price;

        if (newPrice > currentPrice) {
            priceLabel.classList.remove("down-price-update");
            priceLabel.classList.add("up-price-update");
        }
        else if (newPrice < currentPrice) {
            priceLabel.classList.remove("up-price-update");
            priceLabel.classList.add("down-price-update");
        }

        priceLabel.textContent = formatPrice(newPrice);
        priceLabel.dataset.price = newPrice;
    }
}

function updateOwnershipLabels(updatedSymbol, newPrice) {

    let totalPriceOfOwnership = 0;

    ownershipJson.forEach((entry) => {

        if (updatedSymbol === entry.Symbol) {
            entry.TradablePriceInfos.Price = newPrice;
        }

        const totalPrice = entry.TradablePriceInfos.Price * entry.Amount;
        totalPriceOfOwnership += totalPrice;
    })

    const TotalOwnershipValueLabels = document.getElementsByClassName("TotalOwnershipValue");

    for (let totalOwnershipValueLabel of TotalOwnershipValueLabels) {
        totalOwnershipValueLabel.textContent = formatPrice(totalPriceOfOwnership);
    }
}

function onMarketData(message) {

    const tradableUpdatePayload = JSON.parse(message);
    const tradablePriceInfos = tradableUpdatePayload["TradablePriceInfos"];
    const updatedSymbol = tradableUpdatePayload["Symbol"];
    const newPrice = tradablePriceInfos["Price"];

    updatePriceLabels(updatedSymbol, newPrice);

    if (ownershipJson !== null) {
        updateOwnershipLabels(updatedSymbol, newPrice);
    }
}

function initMarketConnection() {

    CurrentConnection = new signalR.HubConnectionBuilder().withUrl("/marketHub").build();

    CurrentConnection.start();
    CurrentConnection.on("ReceiveMarketData", onMarketData);
    window.addEventListener("beforeunload", () => {
        CurrentSocket.close();
    });
}

initMarketConnection();