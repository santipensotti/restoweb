function initMap(coordinates) {
    // Crear el mapa centrado en el primer punto
    var map = L.map('map').setView([coordinates[0].lat, coordinates[0].lng], 13);

    // Cargar las capas del mapa
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 19,
        attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
    }).addTo(map);

    // Iterar por cada coordenada para agregar marcadores
    coordinates.forEach((coord, index) => {
        var popupContent = `
            <strong>ID:</strong> ${coord.id}<br>
            <strong>Nombre:</strong> ${coord.name}<br>
            <strong>Mesas:</strong> ${coord.tablesCount}<br>
            <strong>Latitud:</strong> ${coord.lat}<br>
            <strong>Longitud:</strong> ${coord.lng}
        `;

        var marker = L.marker([coord.lat, coord.lng])
            .addTo(map)
            .bindPopup(popupContent);

        // Registrar un evento 'click' para abrir el popup manualmente
        marker.on('click', function () {
            console.log(`Marcador ${index} clickeado: ID=${coord.id}, Nombre=${coord.name}`);
            this.openPopup();
        });
    });

    console.log("Todos los marcadores fueron añadidos correctamente:", coordinates);
}
