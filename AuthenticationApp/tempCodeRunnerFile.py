import sqlite3

def create_database():
    # Connexion à la base de données (elle sera créée si elle n'existe pas)
    conn = sqlite3.connect('auth.db')
    cursor = conn.cursor()

    # Création de la table Users
    cursor.execute('''
        CREATE TABLE IF NOT EXISTS Users (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Email TEXT NOT NULL UNIQUE,
            Username TEXT NOT NULL UNIQUE,
            PasswordHash TEXT NOT NULL
        )
    ''')

    # Sauvegarde des modifications et fermeture de la connexion
    conn.commit()
    conn.close()

if __name__ == "__main__":
    create_database()
    print("Database created successfully")