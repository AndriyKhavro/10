CREATE DATABASE users_db;

CREATE TABLE IF NOT EXISTS users (
    user_id SERIAL  NOT NULL,
    username VARCHAR(50) NOT NULL,
    email VARCHAR(100) NOT NULL,
    first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    date_of_birth DATE NOT NULL,

    PRIMARY KEY (user_id)
);