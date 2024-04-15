USE sd3;

CREATE TABLE users 
(
  id        VARCHAR(36) NOT NULL,
  name      VARCHAR(200) NOT NULL,
  login     VARCHAR(20) NOT NULL,
  password  VARCHAR(100) NOT NULL,
  PRIMARY KEY (id),
  UNIQUE INDEX `id_UNIQUE` (id ASC) 
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
  UNIQUE INDEX `id_UNIQUE` (id ASC),
  UNIQUE INDEX `title_UNIQUE` (title ASC)
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
    UNIQUE INDEX `id_UNIQUE` (id ASC),
    UNIQUE INDEX `title_UNIQUE` (title ASC)
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
    UNIQUE INDEX `id_UNIQUE` (id ASC)
);

INSERT INTO users (id, name, login, password) 
VALUES ('550e8400-e29b-41d4-a716-446655440000', 'John Doe', 'john_doe', 'password123');

INSERT INTO topics (id, title, description, date, labels, userId) 
VALUES ('550e8400-e29b-41d4-a716-446655440001', 'Study Habits', 'Effective study habits for better learning', '2024-04-08', '["#sleep", "#learning"]', '550e8400-e29b-41d4-a716-446655440000');

INSERT INTO ideas (id, title, description, date, userId, topicId) 
VALUES ('550e8400-e29b-41d4-a716-446655440002', 'Pomodoro Technique', 'Use Pomodoro Technique for focused study sessions', '2024-04-08', '550e8400-e29b-41d4-a716-446655440000', '550e8400-e29b-41d4-a716-446655440001');

INSERT INTO votes (id, positive, userId, ideaId) 
VALUES ('550e8400-e29b-41d4-a716-446655440003', TRUE, '550e8400-e29b-41d4-a716-446655440000', '550e8400-e29b-41d4-a716-446655440002');