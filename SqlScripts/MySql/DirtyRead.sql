BEGIN;
SET SESSION TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
SELECT email FROM users
WHERE user_id = 1;

-- In second transaction:
-- BEGIN
-- UPDATE users SET email = 'dirty@gmail.com' WHERE user_id = 1;

SELECT email FROM users
WHERE user_id = 1;
-- READ UNCOMMITTED in MySql returns updated value of uncommitted transaction.

COMMIT;
