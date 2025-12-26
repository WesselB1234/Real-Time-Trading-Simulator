function onAmountChanged(amountInput, totalValueLbl, newAmount) {

    if (newAmount > 0) {
        amountInput.dataset.amountLabelValue = newAmount;
        totalValueLbl.textContent = FormatPrice(totalValueLbl.dataset.price * newAmount);
    }
}

function initBuySellPriceSetter() {

    let amountInput = document.getElementById("amount");
    let totalValueLbl = document.getElementById("totalValueLbl");

    amountInput.addEventListener("input", (event) => {
        OnAmountChanged(amountInput, totalValueLbl, Number(event.target.value));
    });
}

initBuySellPriceSetter();