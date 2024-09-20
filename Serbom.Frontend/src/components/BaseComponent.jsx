import { Component } from 'react';

export class BaseComponent extends Component {

    get baseUrl() { return 'http://localhost:5080/api'; }

    componentDidMount() {
        this.populate();
    }

    async token() {
        fetch(this.baseUrl + "/token", {
            method: 'GET',
            headers: {
                'Authorization': 'bearer ' + sessionStorage.getItem("jwtkn")
            }
        }).then(response => {
            if (response.ok) {
                return response.json().then(data => {
                    sessionStorage.setItem("jwtkn", data.token);
                });
            } else {
                throw new Error(response.message);
            }
        }).catch((err, a) => { 
            console.log(err.message);
            window.location.replace("/login");
        });
    }

    async post(controller, data, action = "") {
        console.log(data);
        if (action !== "") {
            action = "/" + action;
        }
        return this.token().then(() => {
            return fetch(this.baseUrl + '/' + controller + action, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': 'bearer ' + sessionStorage.getItem("jwtkn")
                },
                body: JSON.stringify(data)
            });
        });
    }

    async get(controller, data = "", action = "") {
        
        if (action !== "") {
            action = "/" + action;
        }
        if(data !== "") {
            data = "/" + data;
        }
        return this.token().then(() => {
            return fetch(this.baseUrl + '/' + controller + action + data, {
                method: 'GET',
                headers: {
                    'Authorization': 'bearer ' + sessionStorage.getItem("jwtkn")
                }
            });
        });
    }

    async put(controller, data, action = "") {
        if (action !== "") {
            action = "/" + action;
        }
        return this.token().then(() => {
            return fetch(this.baseUrl + '/' + controller + action, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': 'bearer ' + sessionStorage.getItem("jwtkn")
                },
                body: JSON.stringify(data)
            });
        });
    }

    async delete(controller, data, action = "") {
        if (action !== "") {
            action = "/" + action;
        }
        return this.token().then(() => {
            return fetch(this.baseUrl + controller + action + '/' + data, {
                method: 'DELETE',
                'Authorization': 'bearer ' + sessionStorage.getItem("jwtkn")
            });
        });
    }

    populate() { }
}