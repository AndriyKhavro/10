BEGIN;
SET SESSION TRANSACTION ISOLATION LEVEL READ COMMITTED;
SELECT * FROM users
WHERE user_id = 1;

-- In second transaction
-- In MySQL if isolation level is SERIALIZABLE, UPDATE waits for the first transaction to be committed.
-- BEGIN
-- UPDATE users SET email = 'nonrepeatable@email.com' WHERE user_id = 1;
-- COMMIT

SELECT * FROM users
WHERE user_id = 1;
-- It returns different value depending on isolation level.
-- READ UNCOMMITTED, READ COMMITTED return updated value, REPEATABLE READ and SERIALIZABLE (in Postgres) returns old value.

COMMIT;