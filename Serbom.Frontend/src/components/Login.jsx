import React from 'react';
import { BaseComponent } from './BaseComponent';
import './Login.css';

export class Login extends BaseComponent {

    constructor(props) {
        super(props);
        this.state = { email: '', password: '' };
        sessionStorage.clear();
    }

    componentDidMount() {
        this.render();
    }

    render() {
        return (
            <div id="Login">
                <form onSubmit={(e) => { e.preventDefault(); this.handleLogin(); return false; }} autoComplete="off">
                    <div>
                        <label htmlFor="email">Email:</label>
                        <input
                            id="email"
                            type="email"
                            value={this.state.email}
                            onChange={ (e) => this.setState({ email: e.target.value, password: this.state.password }) }
                        />
                    </div>
                    <div>
                        <label htmlFor="password">Password:</label>
                        <input
                            id="password"
                            type="password"
                            value={this.state.password}
                            onChange={(e) => this.setState({ email: this.state.email, password: e.target.value }) }
                        />
                    </div>
                    <div>
                        <label></label>
                        <button type="submit">Login</button>
                    </div>
                </form>
            </div>
        );
    }

    async handleLogin() {
        await fetch(this.baseUrl + "/token", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Origin' : window.location.origin
            },
            body: JSON.stringify({ Email: this.state.email, Password: this.state.password })
        }).then(response => {
            if (response.ok) {
                response.json().then(data => {
                    sessionStorage.setItem('jwtkn', data.token);
                    this.state = { email: '', password: '' };
                    window.location.replace("/");
                });
            } else {
                response.json().then(data => {
                    alert("Credenciais inválidas! Tente novamente.");
                });
            }
        }).catch((err) => {
            console.log(err);
            alert("Ocorreu um erro na comunicação com o servidor.");
            this.state = { email: '', password: '' };
        });
    }
}