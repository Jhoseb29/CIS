USE sd3;

CREATE TABLE users 
(
  id        VARCHAR(36) NOT NULL,
  name      VARCHAR(200) NOT NULL,
  login     VARCHAR(20) NOT NULL,
  password  VARCHAR(100) NOT NULL,
  PRIMARY KEY (id),
  UNIQUE INDEX id_UNIQUE (id ASC) 
);

CREATE TABLE topics (
  id            VARCHAR(36) NOT NULL,
  title         VARCHAR(200) NOT NULL,
  description   VARCHAR(500) NOT NULL,
  date          DATETIME NOT NULL,
  labels        JSON NOT NULL,
  userId        VARCHAR(36) NOT NULL,
  PRIMARY KEY (id),
  FOREIGN KEY (userId) REFERENCES users(id),
  UNIQUE INDEX id_UNIQUE (id ASC),
  UNIQUE INDEX title_UNIQUE (title ASC)
);

CREATE TABLE ideas
(
    id              VARCHAR(36) NOT NULL,
    title           VARCHAR(200) NOT NULL,
    description     VARCHAR(500) NOT NULL,
    date            DATETIME NOT NULL,
    userId          VARCHAR(36) NOT NULL,
    topicId          VARCHAR(36) NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY(userId) REFERENCES users(id),
    FOREIGN KEY(topicId) REFERENCES topics(id),
    UNIQUE INDEX id_UNIQUE (id ASC),
    UNIQUE INDEX unique_title_per_topic (title, topicId) 
);

CREATE TABLE votes
(
    id              VARCHAR(36) NOT NULL,
    positive        BOOLEAN NOT NULL,
    userId          VARCHAR(36) NOT NULL,
    ideaId          VARCHAR(36) NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY(userId) REFERENCES users(id),
    FOREIGN KEY(ideaId) REFERENCES ideas(id),
    UNIQUE INDEX id_UNIQUE (id ASC),
    UNIQUE INDEX unique_user_idea_vote (userId, ideaId) 
);

-- TRIGGER PARA BORRAR TOPICOS BORRANDO PRIMERO VOTOS E IDEAS RELACIONADAS AUTOMATICAMENTE
DELIMITER $$
CREATE TRIGGER before_topic_delete
BEFORE DELETE ON topics
FOR EACH ROW
BEGIN
    -- Borra los votos relacionados con las ideas que se están eliminando
    DELETE FROM votes WHERE ideaId IN (SELECT id FROM ideas WHERE topicId = OLD.id);

    -- Borra las ideas relacionadas con el tema que se está eliminando
    DELETE FROM ideas WHERE topicId = OLD.id;

END$$
DELIMITER ;
