CurrentConnection = new signalR.HubConnectionBuilder().withUrl("/marketHub").build();

CurrentConnection.start();
CurrentConnection.on("ReceiveMarketData", function (message) {
    console.log(message);
});
window.addEventListener("beforeunload", () => {
    CurrentSocket.close();
});