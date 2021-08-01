"use strict";

(function () {

	var connection = new signalR.HubConnectionBuilder()
		.withUrl("/chatHub")
		.withAutomaticReconnect()
		.build();

	connection.on("ReceiveMessage", function (user, message) {
		var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
		var encodedMsg = user + " says " + msg;

		var li = document.createElement("li");
		li.textContent = encodedMsg;
		document.getElementById("mensagens").appendChild(li);
	});

	connection.start().catch(function (err) {
		return console.error(err.toString());
	});

	document.getElementById("btn-enviar").addEventListener("click", function (event) {
		var nome = document.getElementById("nome").value;
		var mensagem = document.getElementById("mensagem").value;

		connection.invoke("SendMessage", nome, mensagem).catch(function (err) {
			return console.error(err.toString());
		});
		event.preventDefault();
	});


})();