SET SESSION TRANSACTION ISOLATION LEVEL REPEATABLE READ;
BEGIN;
SELECT username, date_of_birth FROM users
WHERE date_of_birth = '1991-08-24';

-- In second transaction
 -- BEGIN
INSERT INTO users (username, email, first_name, last_name, date_of_birth) VALUES ('Phantom', 'Phantom@gmail.com', 'Phantom', 'Phantom', '1991-08-24');
-- COMMIT second transaction.

-- Generally, phantom reads do not happen in MySql REPEATABLE READ isolation level.
-- However, for some reason such update reproduces it.
-- Found here: https://stackoverflow.com/a/41178461
-- 
UPDATE users SET username = 'Phantom_UPDATED' WHERE user_id = 39439; -- use id inserted in second transaction

SELECT username, date_of_birth FROM users
WHERE date_of_birth = '1991-08-24';

COMMIT;
ROLLBACK