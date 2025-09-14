function OnAmountChanged(amountInput, totalValueLbl, newAmount) {

    if (newAmount > 0) {
        amountInput.dataset.amountLabelValue = newAmount
        totalValueLbl.textContent = FormatPrice(totalValueLbl.dataset.price * newAmount)
    }
}

function InitBuySellPriceSetter() {

    let amountInput = document.getElementById("amount")
    let totalValueLbl = document.getElementById("totalValueLbl")

    amountInput.addEventListener("input", (event) => {
        OnAmountChanged(amountInput, totalValueLbl, Number(event.target.value))
    })
}

InitBuySellPriceSetter();