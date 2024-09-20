function info(text)
{
    console.info('\x1b[34m' + text + ' \x1b[0m');
}
function warning(text)
{
    console.warn('\x1b[33m' + text + ' \x1b[0m');
}
function error(text)
{
    console.error('\x1b[31m' + text + ' \x1b[0m');
}
function isWindows() {
    return process.platform === "win32";
}

module.exports = {info, warning, error, isWindows};
