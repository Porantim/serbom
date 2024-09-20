import React from 'react';
import { BaseComponent } from './BaseComponent';
import { Link } from 'react-router-dom';

export class Home extends BaseComponent {
    static displayName = Home.name;

    constructor(props) {
        super(props);
        this.state = { dataItems: [], loading: true };
    }

    static renderData(dataItems) {
        console.log(dataItems);
        return (
            <table className="table table-striped" aria-labelledby="tableLabel">
                <thead>
                    <tr>
                        <th>Número</th>
                        <th>Tipo</th>
                        <th>Cliente</th>
                        <th>Início</th>
                        <th>Valor</th>
                        <th>Status</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {dataItems.map(itm =>
                        <tr key={itm.id}>
                            <td><Link to={`contract/${itm.id}`} >{itm.number}</Link></td>
                            <td>{itm.type}</td>
                            <td>{itm.start}</td>
                            <td>{itm.client.name}</td>
                            <td>{itm.value}</td>
                            <td>{itm.status}</td>
                            <td>
                                <Link to={`contract/${itm.id}`} >[ ver ]</Link>
                                &nbsp;|&nbsp;
                                <Link to={`contract/${itm.id}/edit`} >[ editar ]</Link>
                            </td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Carregando...</em></p>
            : Home.renderData(this.state.dataItems);

        return (
            <div>
                <h1 id="tableLabel">Contratos</h1>
                {contents}
            </div>
        );
    }

    async populate() {
        return this.get('contract').then(response => {
            if (response.ok) {
                return response.json().then(data => {
                    this.setState({ dataItems: data, loading: false });
                });
            } else {
                console.log(response);
                throw new Error(response.text);
            }
        }).catch((err) => {
            console.log(err.message);
            alert("Ocorreu um erro na comunicação com o servidor.");
        });
    }
}
