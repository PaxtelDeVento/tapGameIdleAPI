DROP DATABASE tapIdleGame;
CREATE DATABASE tapIdleGame;

USE tapIdleGame;

CREATE TABLE Users (
    id INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(100) NOT NULL,
    email VARCHAR(255) NOT NULL,
    password VARCHAR(255) NOT NULL
);

CREATE TABLE Diamonds (
    id INT AUTO_INCREMENT PRIMARY KEY,
    user_id INT NOT NULL,
    total_diamonds DECIMAL(18, 2) DEFAULT 0.00,
    diamonds_per_tap DECIMAL(10, 2) DEFAULT 1.00,
    diamonds_per_second DECIMAL(10, 2) DEFAULT 0.00,
    FOREIGN KEY (user_id) REFERENCES Users(id) ON DELETE CASCADE
);

CREATE TABLE Upgrades (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    description TEXT,
    cost INT,
    cost_increment INT,
    modifier VARCHAR(3) NOT NULL,
    diamonds_increment DECIMAL(10, 2) DEFAULT 0.00,
    type CHAR(1) NOT NULL
);

CREATE TABLE User_Upgrade (
    id INT AUTO_INCREMENT PRIMARY KEY,
    user_id INT NOT NULL,
    upgrade_id INT NOT NULL,
    amount INT DEFAULT 0,
    current_cost INT DEFAULT 0,
    FOREIGN KEY (user_id) REFERENCES Users(id) ON DELETE CASCADE,
    FOREIGN KEY (upgrade_id) REFERENCES Upgrades(id) ON DELETE CASCADE
);

INSERT INTO Users VALUES (1,'Eduardo', 'a','aa');
INSERT INTO Diamonds VALUES (1,1,1000,10,1);

INSERT INTO Users VALUES (2,'Pipe', 'b','bb');
INSERT INTO Diamonds VALUES (2,2,50,5,1);
INSERT INTO Upgrades VALUES 
(1, 'Melhorar picareta', 'Aumenta a quantidade de diamantes por click em 0.5', 10, 5, 'DPC', 0.5, '+'),
(2, 'Contratar mineiro', 'Aumenta a quantidade de diamantes por segundo em 1', 30, 20, 'DPS', 1.0, '+');

INSERT INTO User_Upgrade VALUES(1, 1, 1, 1, 10);
INSERT INTO User_Upgrade VALUES(2, 1, 2, 1, 30);
INSERT INTO User_Upgrade VALUES(3, 2, 1, 2, 10);
INSERT INTO User_Upgrade VALUES(4, 2, 2, 2, 30);

SELECT * FROM Users;
SELECT * FROM Diamonds;
SELECT * FROM Upgrades;
SELECT * FROM User_Upgrade;

DELIMITER //

CREATE TRIGGER add_user_upgrades_after_insert
AFTER INSERT ON Users
FOR EACH ROW
BEGIN
  -- Insere um registro na tabela User_Upgrade para cada upgrade existente
  INSERT INTO User_Upgrade (user_id, upgrade_id, amount, current_cost)
  SELECT NEW.id, u.id, 0, u.cost
  FROM Upgrades u;
END //

DELIMITER ;
