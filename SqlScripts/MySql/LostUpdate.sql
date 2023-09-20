BEGIN;
SET SESSION TRANSACTION ISOLATION LEVEL REPEATABLE READ;

SELECT * FROM users
WHERE user_id = 1;

-- Do the same from second transaction.

UPDATE users SET email = 'email4@email.com' WHERE user_id = 1;
-- For REPEATABLE READ, UPDATE from the second transaction waits for the first transaction to complete.
-- In Postgres, it fails after first transaction is committed.
-- In MySQL, it succeeds, which leads to lost update from the first transaction.

COMMIT;

SELECT * FROM users
WHERE user_id = 1;