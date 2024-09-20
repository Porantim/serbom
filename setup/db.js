'use strict';

const fs = require('fs');
const { exit } = require("process");
const env = require('./environment');
const mysql = require('mysql2');

env.info('Connect to MySQL...');

let conn = mysql.createConnection({
    host: 'localhost',
    port: '3306',
    database: 'Serbom',
    user: 'serbom',
    password: 'serbompwd'
 });

try {
    env.info('Creating database schema...');
    {
        const tablesContent = fs.readFileSync('./create.sql', 'utf-8');

        let lines = tablesContent.split("\n");
        let cache = '';

        lines.forEach((line) => {
            let trimmedLine = line.replace(/\s+/gm, ' ').trim();
            if (!trimmedLine.startsWith('#') && trimmedLine.length > 0) {
                cache = (cache + ' ' + trimmedLine).trim();
                if (trimmedLine.endsWith(';')) {
                    conn.query(cache);
                    cache = '';
                }
            }
        });
    }

    let exists = false;
    conn.query("SELECT COUNT(id) FROM contractType", function (err, results) {
        if (err) throw err;
        if (results.count > 0) exists = true;
    });
    if (!exists) {
        conn.query(`INSERT INTO contractType (name, description) 
                            VALUES ('Aluguel', 'Contratos de aluguel de máquinas ou equipamentos'),
                                    ('Compra e Venda', 'Contratos de compra e venda de produtos'),
                                    ('Prestação de Serviços', 'Contratos de prestação de serviços'),
                                    ('Empreitada', 'Contratos de empreitada'),
                                    ('Doação', 'Contratos de doação'),
                                    ('Permuta', 'Contratos de permuta'),
                                    ('Comodato', 'Contratos de comodato'),
                                    ('Parceria', 'Contratos de parceria'),
                                    ('Fornecimento', 'Contratos de fornecimento')`);
    }

    exists = false;
    conn.query("SELECT COUNT(id) FROM contractStatus", function (err, results) {
        if (err) throw err;
        if (results.count > 0) exists = true;
    });
    if (!exists) {
        conn.query(`INSERT INTO contractStatus (description)
                            VALUES ('Ativo'),
                                    ('Inativo'),
                                    ('Cancelado'),
                                    ('Suspenso'),
                                    ('Encerrado'),
                                    ('Em Análise'),
                                    ('Em Aprovação')`);
    }

} catch (err) {
    env.error("Error: " + err.message);
    exit(0);
} finally {
    conn.end();
}

env.info('Database schema created!');