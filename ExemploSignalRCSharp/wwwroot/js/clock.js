"use strict";

(function () {

	var connection = new signalR.HubConnectionBuilder()
		.withUrl("/clockHub")
		.withAutomaticReconnect()
		.build();

	connection.start().catch(function (err) {
		return console.error(err.toString());
	});


	connection.on("ReceiveTime", (time) => {
		//console.log('Recebido: ', time);

		var clock = document.getElementById("clock");

		clock.innerHTML = time;
	});

})();
