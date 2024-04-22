# Simple Data Examples
INSERT INTO users (id, name, login, password) 
VALUES ('550e8400-e29b-41d4-a716-446655440000', 'John Doe', 'john_doe', 'password123');

INSERT INTO topics (id, title, description, date, labels, userId) 
VALUES ('550e8400-e29b-41d4-a716-446655440001', 'Study Habits', 'Effective study habits for better learning', '2024-04-08', '["#sleep", "#learning"]', '550e8400-e29b-41d4-a716-446655440000');

INSERT INTO ideas (id, title, description, date, userId, topicId) 
VALUES ('550e8400-e29b-41d4-a716-446655440002', 'Pomodoro Technique', 'Use Pomodoro Technique for focused study sessions', '2024-04-08', '550e8400-e29b-41d4-a716-446655440000', '550e8400-e29b-41d4-a716-446655440001');

INSERT INTO votes (id, positive, userId, ideaId) 
VALUES ('550e8400-e29b-41d4-a716-446655440003', TRUE, '550e8400-e29b-41d4-a716-446655440000', '550e8400-e29b-41d4-a716-446655440002');



# Complex Data Examples
-- 10 rows into the 'topics' table
INSERT INTO topics (id, title, description, date, labels, userId)
VALUES
    (UUID(), 'How to Create a Personal Budget', 'Learn how to manage your finances effectively by creating a personal budget.', NOW(), '["finance", "budget", "money"]', (SELECT id FROM users ORDER BY RAND() LIMIT 1)),
    (UUID(), 'Introduction to Machine Learning', 'Discover the basics of machine learning and its applications in various industries.', NOW(), '["machine learning", "data science", "technology"]', (SELECT id FROM users ORDER BY RAND() LIMIT 1)),
    (UUID(), 'Healthy Cooking Recipes', 'Explore nutritious and delicious recipes for a balanced diet and healthy lifestyle.', NOW(), '["cooking", "recipes", "healthy living"]', (SELECT id FROM users ORDER BY RAND() LIMIT 1)),
    (UUID(), 'Travel Photography Tips', 'Learn essential tips and techniques for capturing stunning travel photos during your adventures.', NOW(), '["photography", "travel", "tips"]', (SELECT id FROM users ORDER BY RAND() LIMIT 1)),
    (UUID(), 'Effective Time Management Strategies', 'Master time management skills to increase productivity and achieve your goals efficiently.', NOW(), '["time management", "productivity", "self-improvement"]', (SELECT id FROM users ORDER BY RAND() LIMIT 1)),
    (UUID(), 'Investing in Real Estate', 'Explore the fundamentals of real estate investment and strategies for building wealth through property.', NOW(), '["real estate", "investment", "finance"]', (SELECT id FROM users ORDER BY RAND() LIMIT 1)),
    (UUID(), 'Beginners Guide to Yoga', 'Discover the benefits of yoga for physical and mental well-being with this comprehensive beginners guide.', NOW(), '["yoga", "health", "wellness"]', (SELECT id FROM users ORDER BY RAND() LIMIT 1)),
    (UUID(), 'Artificial Intelligence in Healthcare', 'Explore the transformative potential of AI technology in revolutionizing healthcare systems and patient care.', NOW(), '["artificial intelligence", "healthcare", "technology"]', (SELECT id FROM users ORDER BY RAND() LIMIT 1)),
    (UUID(), 'Mastering Web Development', 'Gain expertise in web development technologies and frameworks to build dynamic and responsive websites.', NOW(), '["web development", "programming", "technology"]', (SELECT id FROM users ORDER BY RAND() LIMIT 1)),
    (UUID(), 'Effective Communication Skills', 'Learn essential communication techniques for building strong relationships and achieving success in both personal and professional life.', NOW(), '["communication", "skills", "self-improvement"]', (SELECT id FROM users ORDER BY RAND() LIMIT 1));

-- 10 rows into the 'ideas' table with unique and relevant proposals for each topic
INSERT INTO ideas (id, title, description, date, userId, topicId)
VALUES
    (UUID(), 'Student Budgeting App with Expense Tracking', 'Design and develop a mobile application tailored specifically for students, allowing them to manage their finances effectively with features such as budget planning and expense tracking.', NOW(), (SELECT id FROM users ORDER BY RAND() LIMIT 1), (SELECT id FROM topics WHERE JSON_SEARCH(labels, 'one', 'finance') IS NOT NULL ORDER BY RAND() LIMIT 1)),
    (UUID(), 'Stock Market Prediction System using LSTM', 'Implement a predictive analytics system leveraging Long Short-Term Memory (LSTM) neural networks to forecast stock prices with improved accuracy and reliability.', NOW(), (SELECT id FROM users ORDER BY RAND() LIMIT 1), (SELECT id FROM topics WHERE JSON_SEARCH(labels, 'one', 'machine learning') IS NOT NULL ORDER BY RAND() LIMIT 1)),
    (UUID(), 'Plant-Based Meal Prep Cookbook for Busy Professionals', 'Create a comprehensive cookbook containing a variety of plant-based meal prep recipes tailored for busy professionals, emphasizing nutrition, convenience, and delicious flavors.', NOW(), (SELECT id FROM users ORDER BY RAND() LIMIT 1), (SELECT id FROM topics WHERE JSON_SEARCH(labels, 'one', 'cooking') IS NOT NULL ORDER BY RAND() LIMIT 1)),
    (UUID(), 'Landscape Photography: Capturing the Essence of Nature', 'Explore advanced techniques and artistic approaches to landscape photography, focusing on capturing the raw beauty and essence of natural landscapes.', NOW(), (SELECT id FROM users ORDER BY RAND() LIMIT 1), (SELECT id FROM topics WHERE JSON_SEARCH(labels, 'one', 'photography') IS NOT NULL ORDER BY RAND() LIMIT 1)),
    (UUID(), 'Time Blocking Method for Enhanced Productivity', 'Introduce a practical time management technique known as time blocking to optimize productivity and goal achievement by allocating dedicated time blocks for specific tasks and activities.', NOW(), (SELECT id FROM users ORDER BY RAND() LIMIT 1), (SELECT id FROM topics WHERE JSON_SEARCH(labels, 'one', 'productivity') IS NOT NULL ORDER BY RAND() LIMIT 1)),
    (UUID(), 'Real Estate Investment Strategies for First-Time Investors', 'Provide valuable insights and expert guidance on real estate investment strategies tailored for first-time investors, covering topics such as property selection, financing options, and risk management.', NOW(), (SELECT id FROM users ORDER BY RAND() LIMIT 1), (SELECT id FROM topics WHERE JSON_SEARCH(labels, 'one', 'real estate') IS NOT NULL ORDER BY RAND() LIMIT 1)),
    (UUID(), 'Yoga for Stress Relief and Inner Peace', 'Develop a comprehensive beginner\'s guide to yoga focusing on stress relief techniques, mindfulness practices, and holistic wellness to promote inner peace and emotional well-being.', NOW(), (SELECT id FROM users ORDER BY RAND() LIMIT 1), (SELECT id FROM topics WHERE JSON_SEARCH(labels, 'one', 'yoga') IS NOT NULL ORDER BY RAND() LIMIT 1)),
    (UUID(), 'AI-Powered Healthcare Diagnosis and Treatment', 'Propose an innovative healthcare solution leveraging artificial intelligence for accurate diagnosis, personalized treatment recommendations, and improved patient care outcomes.', NOW(), (SELECT id FROM users ORDER BY RAND() LIMIT 1), (SELECT id FROM topics WHERE JSON_SEARCH(labels, 'one', 'healthcare') IS NOT NULL ORDER BY RAND() LIMIT 1)),
    (UUID(), 'Advanced Web Development Techniques and Best Practices', 'Explore advanced concepts and best practices in web development, including performance optimization, responsive design principles, and cutting-edge technologies for building dynamic and interactive websites.', NOW(), (SELECT id FROM users ORDER BY RAND() LIMIT 1), (SELECT id FROM topics WHERE JSON_SEARCH(labels, 'one', 'web development') IS NOT NULL ORDER BY RAND() LIMIT 1)),
    (UUID(), 'Mastering Effective Communication in the Digital Age', 'Delve into strategies and techniques for effective communication in today\'s digital landscape, covering topics such as virtual communication, online collaboration tools, and remote team management.', NOW(), (SELECT id FROM users ORDER BY RAND() LIMIT 1), (SELECT id FROM topics WHERE JSON_SEARCH(labels, 'one', 'communication') IS NOT NULL ORDER BY RAND() LIMIT 1));

-- 10 rows into the 'votes' table with random positive and negative votes for each idea
INSERT INTO votes (id, positive, userId, ideaId)
SELECT UUID(), IF(RAND() < 0.5, 1, 0) AS positive, random_user.userId, ideas_sample.id AS ideaId
FROM (
    SELECT id AS userId FROM users ORDER BY RAND() LIMIT 1
) AS random_user
CROSS JOIN (SELECT * FROM ideas LIMIT 10) AS ideas_sample
WHERE NOT EXISTS (
    SELECT 1
    FROM votes
    WHERE votes.userId = random_user.userId
    AND votes.ideaId = ideas_sample.id
);



# INVALID DATA EXAMPLES. THESE EXAMPLES ARE MADE TO FAIL. 
# THESE FOLLOWING EXAMPLES SHOULD FAIL IF YOU HAVE PROPERLY CREATED THE DB WITH THE QUERIES ABOVE .

-- User tries to create a topic with repeated title.
INSERT INTO topics (id, title, description, date, labels, userId) 
VALUES ('550e8400-e29b-41d4-a716-446655440004', 'Study Habits', 'This a wrong example', '2024-04-08', '["cryAllNight"]', '550e8400-e29b-41d4-a716-446655440000');

-- User tries to create an idea with repeated title within a same topic.
INSERT INTO ideas (id, title, description, date, userId, topicId) 
VALUES ('550e8400-e29b-41d4-a716-446655440005', 'Pomodoro Technique', 'This is a repeated technique', '2024-04-08', '550e8400-e29b-41d4-a716-446655440000', '550e8400-e29b-41d4-a716-446655440001');

-- User tries to vote for a same Idea again.
INSERT INTO votes (id, positive, userId, ideaId) 
VALUES ('550e8400-e29b-41d4-a716-446655440006', FALSE, '550e8400-e29b-41d4-a716-446655440000', '550e8400-e29b-41d4-a716-446655440002');