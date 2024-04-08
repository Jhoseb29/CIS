USE sd3;

CREATE TABLE users 
(
  id        VARCHAR(36) NOT NULL,
  name      VARCHAR(200) NOT NULL,
  login     VARCHAR(20) NOT NULL,
  password  VARCHAR(100) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC) 
);

CREATE TABLE topics
(
    id              VARCHAR(36) NOT NULL,
    title           VARCHAR(200) NOT NULL,
    description     VARCHAR(500) NOT NULL,
    date            DATE NOT NULL,
    labels          JSON NOT NULL,
    userId          VARCHAR(36) NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY(userId) REFERENCES users(id),
    UNIQUE INDEX id_UNIQUE (id ASC)
);

CREATE TABLE VideoGames
(
    id              VARCHAR(36) NOT NULL PRIMARY KEY,
    name            VARCHAR(200) NOT NULL,
    platform        VARCHAR(200) NOT NULL,
    price           DOUBLE NOT NULL,
    UNIQUE INDEX `id_UNIQUE` (id ASC)
);


INSERT INTO VideoGames (id, name, platform, price)
VALUES ('d290f1ee-6c54-4b01-90e6-d701748f0851', 'Minecraft', 'XBOX', 55.25);

DELETE FROM VideoGames;