﻿const login = async () => {
    const username = document.getElementById('username').value;
    const password = document.getElementById('password').value;

    const response = await fetch('/Auth/Login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ username, password }) // Envoi de l'objet JSON au backend
    });

    if (response.ok) {
        alert('Login successful');
    } else {
        const errorText = await response.text(); // Récupère le message d'erreur
        alert(errorText); // Affiche le message d'erreur retourné par le serveur
    }
};