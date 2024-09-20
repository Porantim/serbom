CREATE TABLE IF NOT EXISTS `user` (
    `id`             INT           NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `name`           VARCHAR(255)  NOT NULL,
    `email`          VARCHAR(255)  NOT NULL UNIQUE,
    `secret`         CHAR(64)      NOT NULL,
    `salt`           CHAR(24)      NOT NULL,
    `active`         BOOLEAN       NOT NULL DEFAULT TRUE
) ENGINE InnoDB
  DEFAULT CHARACTER SET utf8mb4;

CREATE TABLE IF NOT EXISTS `client` (
    `id`             INT             NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `name`           VARCHAR(255)    NOT NULL,
    `type`           ENUM('individual', 'corporate') NOT NULL,
    `document`       VARCHAR(50)     NOT NULL,
    `email`          VARCHAR(255)    NOT NULL,
    `phone`          VARCHAR(25)     NOT NULL,
    `zipCode`        CHAR(8)         NOT NULL,
    `state`          ENUM('AC', 'AL', 'AM', 'AP', 'BA', 'CE', 'DF', 'ES', 'GO', 'MA', 'MG', 'MS', 'MT', 
                          'PA', 'PB', 'PE', 'PI', 'PR', 'RJ', 'RN', 'RO', 'RR', 'RS', 'SC', 'SE', 'SP', 'TO'
                          ),
    `city`           VARCHAR(255)    NOT NULL,
    `address1`       VARCHAR(255)    NOT NULL,
    `address2`       VARCHAR(255)        NULL
) ENGINE InnoDB
  DEFAULT CHARACTER SET utf8mb4;

CREATE TABLE IF NOT EXISTS `contractType` (
    `id`             TINYINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `name`           VARCHAR(100)  NOT NULL,
    `description`    TEXT          NOT NULL
) ENGINE InnoDB
  DEFAULT CHARACTER SET utf8mb4;

CREATE TABLE IF NOT EXISTS `contractStatus` (
    `id`             TINYINT       NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `description`    VARCHAR(50)   NOT NULL
) ENGINE InnoDB
  DEFAULT CHARACTER SET utf8mb4;

CREATE TABLE IF NOT EXISTS `contract` (
    `id`             INT           NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `client`         INT           NOT NULL REFERENCES client(id),
    `type`           TINYINT       NOT NULL REFERENCES contractType(id),
    `number`         VARCHAR(50)   NOT NULL UNIQUE,
    `subject`        VARCHAR(500)  NOT NULL,
    `start`          DATE          NOT NULL,
    `end`            DATE              NULL,
    `value`          DECIMAL(10,2) NOT NULL,
    `status`         TINYINT       NOT NULL REFERENCES contractStatus(id),
    `conditions`     TEXT              NULL
) ENGINE InnoDB
  DEFAULT CHARACTER SET utf8mb4;

CREATE TABLE IF NOT EXISTS `amendment` (
    `id`             INT           NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `contract`       INT           NOT NULL REFERENCES contract(id),
    `deleted`        BOOLEAN       NOT NULL DEFAULT FALSE,
    `number`         VARCHAR(50)   NOT NULL,
    `description`    VARCHAR(500)  NOT NULL,
    `date`           DATE          NOT NULL,
    `value`          DECIMAL(10,2) NOT NULL,
    `conditions`     TEXT              NULL
) ENGINE InnoDB
  DEFAULT CHARACTER SET utf8mb4;

CREATE TABLE IF NOT EXISTS `annex` (
    `id`             INT           NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `contract`       INT           NOT NULL REFERENCES contract(id),
    `amendment`      INT               NULL REFERENCES amendment(id),
    `name`           VARCHAR(255)  NOT NULL,
    `deleted`        BOOLEAN       NOT NULL DEFAULT FALSE,
    `content`        MEDIUMBLOB    NOT NULL
) ENGINE InnoDB
  DEFAULT CHARACTER SET utf8mb4;

CREATE TABLE IF NOT EXISTS `history` (
    `id`             BIGINT        NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `entityType`     ENUM('user', 'client', 'contract', 'amendment', 'annex') NOT NULL,
    `entityId`       INT           NOT NULL,
    `user`           INT           NOT NULL REFERENCES user(id),
    `date`           DATETIME      NOT NULL,
    `action`         ENUM('create', 'update', 'delete') NOT NULL,
    INDEX(`entityType` ASC, `entityId` ASC),
    INDEX(`date` ASC)
) ENGINE InnoDB
  DEFAULT CHARACTER SET utf8mb4;