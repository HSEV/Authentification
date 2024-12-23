const login = async () => {
    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;

    const response = await fetch('/Auth/Login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ email, password }) // Envoi de l'objet JSON au backend
    });

    if (response.ok) {
        alert('Login successful');
    } else {
        const errorText = await response.text(); // Récupère le message d'erreur
        alert(errorText); // Affiche le message d'erreur retourné par le serveur
    }
};

const register = async (event) => {
    event.preventDefault(); // Empêche le rechargement de la page

    const username = document.getElementById('username').value;
    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;

    const response = await fetch('/Auth/Register', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ username, email, password }) // Envoi de l'objet JSON au backend
    });

    if (response.ok) {
        alert('Account created successfully');
        window.location.href = '/index.html'; // Redirige vers la page de connexion
    } else {
        const errorText = await response.text(); // Récupère le message d'erreur
        alert(errorText); // Affiche le message d'erreur retourné par le serveur
    }
};