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

    const messageElement = document.getElementById('message');

    if (response.ok) {
        const result = await response.json();
        messageElement.innerHTML = `<p style="color: green;">${result.message}</p>`;
        // Optionnel : Rediriger vers la page de connexion après un délai
        setTimeout(() => {
            window.location.href = '/index.html';
        }, 2000);
    } else {
        const errorText = await response.text(); // Récupère le message d'erreur
        messageElement.innerHTML = `<p style="color: red;">${errorText}</p>`;
    }
};


const resetPassword = async (event) => {
    event.preventDefault(); // Empêche le rechargement de la page

    const email = document.getElementById('email').value;
    const newPassword = document.getElementById('new-password').value;

    const response = await fetch('/Auth/ResetPassword', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ email, newPassword }) // Envoi de l'objet JSON au backend
    });

    const messageElement = document.getElementById('message');

    if (response.ok) {
        const result = await response.json();
        messageElement.innerHTML = `<p style="color: green;">${result.message}</p>`;
        // Redirige vers la page de connexion après un délai
        setTimeout(() => {
            window.location.href = '/index.html';
        }, 2000);
    } else {
        const errorText = await response.text(); // Récupère le message d'erreur
        messageElement.innerHTML = `<p style="color: red;">${errorText}</p>`;
    }
};