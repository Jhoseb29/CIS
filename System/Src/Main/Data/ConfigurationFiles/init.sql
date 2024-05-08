Create DATABASE sd3;
USE sd3;

CREATE TABLE users 
(
  id        VARCHAR(36) NOT NULL,
  name      VARCHAR(200) NOT NULL,
  login     VARCHAR(50) NOT NULL,
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


# TEMP TABLES

CREATE TEMPORARY TABLE global_first_names (
    id INT AUTO_INCREMENT PRIMARY KEY,
    first_name VARCHAR(100)
);

CREATE TEMPORARY TABLE global_last_names (
    id INT AUTO_INCREMENT PRIMARY KEY,
    last_name VARCHAR(100)
);

CREATE TEMPORARY TABLE global_topic_titles (
    id INT AUTO_INCREMENT PRIMARY KEY,
    topic_title VARCHAR(200)
);

CREATE TEMPORARY TABLE global_labels (
    id INT AUTO_INCREMENT PRIMARY KEY,
    label VARCHAR(50)
);

CREATE TEMPORARY TABLE global_topic_descriptions (
    topic_title VARCHAR(200),
    description VARCHAR(500)
);

CREATE TEMPORARY TABLE temp_idea_titles (
    id INT AUTO_INCREMENT PRIMARY KEY,
    title VARCHAR(200) NOT NULL
);

CREATE TEMPORARY TABLE temp_idea_descriptions (
    id INT AUTO_INCREMENT PRIMARY KEY,
    description VARCHAR(500) NOT NULL
);


# TRIGGERS
-- TRIGGER PARA BORRAR TOPICOS BORRANDO PRIMERO VOTOS E IDEAS RELACIONADAS AUTOMATICAMENTE
DELIMITER $$
CREATE TRIGGER before_user_delete
BEFORE DELETE ON users
FOR EACH ROW
BEGIN
    -- Borra los votos relacionados con las ideas que se están eliminando
    DELETE FROM topics WHERE userId IN (SELECT id FROM topics WHERE userId = OLD.id);
    DELETE FROM ideas WHERE userId IN (SELECT id FROM ideas WHERE userId = OLD.id);
    DELETE FROM votes WHERE userId IN (SELECT id FROM votes WHERE userId = OLD.id);
END$$
DELIMITER ;

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

-- TRIGGER PARA BORRAR UNA IDEA Y LOS VOTOS RELACIONADOS A ESTA AUTOMATICAMENTE
DELIMITER $$
CREATE TRIGGER before_idea_delete
    BEFORE DELETE ON ideas
    FOR EACH ROW
BEGIN
    -- Borra los votos relacionados con la idea que se está eliminando
    DELETE FROM votes WHERE ideaId = OLD.id;
END$$
DELIMITER ;















# PROCEDURES



DELIMITER //
CREATE PROCEDURE InsertMultipleTopics(IN num_topics INT)
BEGIN
    DECLARE i INT DEFAULT 0;
    
    WHILE i < num_topics DO
        INSERT INTO topics (id, title, description, date, labels, userId)
        SELECT 
            UUID(), 
            CONCAT(gtt.topic_title, SUBSTRING(MD5(RAND()) FROM 1 FOR 5)),
            gtd.description,
            NOW(),
            JSON_ARRAYAGG(gl.label),
            (SELECT id FROM users ORDER BY RAND() LIMIT 1)
        FROM 
            (SELECT topic_title 
             FROM global_topic_titles 
             ORDER BY RAND() 
             LIMIT 1) AS gtt
            CROSS JOIN (
                SELECT description
                FROM global_topic_descriptions
                ORDER BY RAND()
                LIMIT 1
            ) AS gtd
            CROSS JOIN (
                SELECT label
                FROM (
                    SELECT label
                    FROM global_labels
                    ORDER BY RAND()
                    LIMIT 40
                ) AS shuffled_labels
                ORDER BY RAND()
                LIMIT 3
            ) AS gl
        GROUP BY gtt.topic_title, gtd.description
		ORDER BY RAND()
        LIMIT 1;
        
        SET i = i + 1;
    END WHILE;
    
END //

DELIMITER ;

DELIMITER //

CREATE PROCEDURE InsertMultipleIdeas(IN num_iterations INT)
BEGIN
    DECLARE i INT DEFAULT 0;
    
    WHILE i < num_iterations DO
        INSERT INTO ideas (id, title, description, date, userId, topicId)
        SELECT 
            UUID(), 
            CONCAT(tit.title, SUBSTRING(MD5(RAND()) FROM 1 FOR 5)),
            descrip.description,
            NOW(),
            (SELECT id FROM users ORDER BY RAND() LIMIT 1),
            (SELECT id FROM topics ORDER BY RAND() LIMIT 1)
        FROM 
            (SELECT title FROM temp_idea_titles ORDER BY RAND() LIMIT 1) AS tit,
            (SELECT description FROM temp_idea_descriptions ORDER BY RAND() LIMIT 1) AS descrip;
        
        SET i = i + 1;
    END WHILE;
    
END //

DELIMITER ;