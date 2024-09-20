import React from 'react';
import { BaseComponent } from './BaseComponent';
import './Contract.css';

export class Contract extends BaseComponent {
    static displayName = "Contratos";

    constructor(props) {
        super(props);

        let contractId = 0;

        if (!props.action) {
            props.action = 'new';
        }
        if (props.action !== 'new') {
            contractId = parseInt(window.location.pathname.trim().substring(1).split('/')[1]);
        }

        this.state = {
            contractId: contractId,
            action: props.action,
            contract: {
                "type": 1,
                "number": '',
                "subject": '',
                "start": '',
                "value": 1,
                "status": 1,
                "conditions": ''
            },
            client: {
                "name": '',
                "type": 'individual',
                "email": '',
                "phone": '',
                "zipCode": '',
                "state": '',
                "city": '',
                "address1": '',
                "address2": '',
                "document": ''
            },
            contractLoaded: props.action === 'new',
            clientLoaded: props.action === 'new',
            statuses: [],
            types: [],
            combosLoaded: false
        };
    }

    componentDidMount() {
        this.populate();
    }

    renderContract() {
        return (
            <div>
                <h3>Dados do contrato</h3>
                <div>
                    <label htmlFor="number">N&uacute;mero</label>
                    {this.state.action === 'view' &&
                        <span id="number">{this.state.contract.number}</span>
                    }
                    {this.state.action !== 'view' &&
                        <input
                            id="number"
                            type="text"
                            value={this.state.contract.number}
                            onChange={(e) => {
                                let nState = this.state;
                                nState.contract.number = e.target.value;
                                this.setState(nState);
                            }}
                        />
                    }
                </div>
                <div>
                    <label htmlFor="type">Tipo</label>
                    {this.state.action === 'view' &&
                        <span id="type">{this.state.types.find(s => s.id === this.state.contract.type).name}</span>
                    }
                    {this.state.action !== 'view' &&
                        <select
                            id="type"
                            value={this.state.contract.type}
                            onChange={(e) => {
                                let nState = this.state;
                                nState.contract.type = e.target.value;
                                this.setState(nState);
                            }}
                        >
                            {this.state.types.map((itm, index) => {
                                return <option key={`type_${itm.id}`} value={itm.id}>{itm.name}</option>;
                            })}
                        </select>
                    }
                
                </div>
                <div>
                    <label htmlFor="subject">Objeto do contrato</label>
                    {this.state.action === 'view' &&
                        <span id="subject">{this.state.contract.subject}</span>
                    }
                    {this.state.action !== 'view' &&
                        <input
                            id="subject"
                            type="text"
                            value={this.state.contract.subject}
                            onChange={(e) => {
                                let nState = this.state;
                                nState.contract.subject = e.target.value;
                                this.setState(nState);
                            }}
                        />
                    }
                </div>
                <div>
                    <label htmlFor="start">Data de in&iacute;cio</label>
                    {this.state.action === 'view' &&
                        <span id="start">{this.state.contract.start}</span>
                    }
                    {this.state.action !== 'view' &&
                        <input
                            id="start"
                            type="date"
                            value={this.state.contract.start ? new Date(this.state.contract.start).toISOString().split('T')[0] : ''}
                            onChange={(e) => {
                                let nState = this.state;
                                nState.contract.start = e.target.valueAsDate;
                                this.setState(nState);
                            }}
                        />
                    }
                </div>
                <div>
                    <label htmlFor="value">Valor do contrato</label>
                    {this.state.action === 'view' &&
                        <span id="value">R$ {this.state.contract.value}</span>
                    }
                    {this.state.action !== 'view' &&
                        <input
                            id="value"
                            type="number"
                            step="0.01"
                            value={this.state.contract.value}
                            onChange={(e) => {
                                let nState = this.state;
                                nState.contract.value = e.target.value.valueAsNumber
                                this.setState(nState);
                            }}
                        />
                    }
                </div>
                <div>
                    <label htmlFor="status">Status</label>
                    {this.state.action === 'view' &&
                        <span id="status">{this.state.statuses.find(s => s.id === this.state.contract.status).description}</span>
                    }
                    {this.state.action !== 'view' &&
                        <select
                            id="status"
                            value={this.state.contract.status}
                            onChange={(e) => {
                                this.state.contract.status = e.target.value;
                                this.setState(this.state);
                            }}
                        >
                            {this.state.statuses.map((itm, index) => {
                                return <option key={`status_${itm.id}`} value={itm.id}>{itm.description}</option>;
                            })}
                        </select>
                    }
                </div>
                <div>
                    <label htmlFor="conditions">Condi&ccedil;&otilde;es do contrato</label>
                    {this.state.action === 'view' &&
                        <span id="conditions">{this.state.contract.conditions}</span>
                    }
                    {this.state.action !== 'view' &&
                        <textarea
                            id="conditions"
                            type="textarea"
                            rows="5"
                            value={this.state.contract.conditions}
                            onChange={(e) => {
                                let nState = this.state;
                                nState.contract.conditions = e.target.value;
                                this.setState(nState);
                            }}
                        />
                    }
                </div>
            </div>
        );
    }

    renderClient(client) {
        return (
            <div>
                <h3>Dados do cliente</h3>
                <div>
                    <label htmlFor="clientDocument">Documento</label>
                    {this.state.action === 'view' &&
                        <span id="clientDocument">{this.state.client.document}</span>
                    }
                    {this.state.action !== 'view' &&
                        <input
                            id="clientDocument"
                            type="number"
                            value={this.state.client.document}
                            onChange={(e) => {
                                let nState = this.state;
                                nState.client.document = e.target.value;
                                this.setState(nState);
                                this.searchClient();
                            }}
                        />
                    }
                </div>
                <div>
                    <label htmlFor="clientName">Nome</label>
                    {this.state.action === 'view' &&
                        <span id="clientName">{this.state.client.name}</span>
                    }
                    {this.state.action !== 'view' &&
                        <input
                            id="clientName"
                            type="text"
                            value={this.state.client.name}
                            onChange={(e) => {
                                let nState = this.state;
                                nState.client.name = e.target.value;
                                this.setState(nState);
                            }}
                        />
                    }
                </div>
                <div>
                    <label htmlFor="clientType">Tipo</label>
                    {this.state.action === 'view' &&
                        <span id="clientType">
                            {this.state.client.type === 'individual' ? "Pessoa Física" : "Pessoa Jurídica"}
                        </span>
                    }
                    {this.state.action !== 'view' &&
                        <select
                            id="clientType"
                            value={this.state.client.type}
                            onChange={(e) => {
                                this.state.client.type= e.target.value;
                                this.setState(this.state);
                            }}
                        >
                            <option key="clientType_individual" value="individual">Pessoa F&iacute;sica</option>
                            <option key="clientType_corporate" value="corporate">Pessoa Jur&iacute;dica</option>
                        </select>
                    }
                </div>
                <div>
                    <label htmlFor="clientZipCode">CEP</label>
                    {this.state.action === 'view' &&
                        <span id="clientZipCode">{this.state.client.zipCode}</span>
                    }
                    {this.state.action !== 'view' &&
                        <input
                            id="clientZipCode"
                        type="number"
                            max="99999999"
                            value={this.state.client.zipCode}
                            maxLength="8"
                            onChange={(e) => {
                                let nState = this.state;
                                nState.client.zipCode = e.target.value;
                                this.setState(nState);
                                this.findAddress();
                            }}
                        />
                    }
                </div>
                <div>
                    <label htmlFor="clientAddress1">Endere&ccedil;o</label>
                    {this.state.action === 'view' &&
                        <span id="clientAddress1">{this.state.client.address1}</span>
                    }
                    {this.state.action !== 'view' &&
                        <input
                            id="clientAddress1"
                            type="text"
                            value={this.state.client.address1}
                            onChange={(e) => {
                                let nState = this.state;
                                nState.client.address1 = e.target.value;
                                this.setState(nState);
                            }}
                        />
                    }
                </div>
                <div>
                    <label htmlFor="clientAddress2">Complemento</label>
                    {this.state.action === 'view' &&
                        <span id="clientAddress2">{this.state.client.address2}</span>
                    }
                    {this.state.action !== 'view' &&
                        <input
                            id="clientAddress2"
                            type="text"
                            value={this.state.client.address2}
                            onChange={(e) => {
                                let nState = this.state;
                                nState.client.address2 = e.target.value;
                                this.setState(nState);
                            }}
                        />
                    }
                </div>
                <div>
                    <label htmlFor="clientCity">Cidade</label>
                    {this.state.action === 'view' &&
                        <span id="clientCity">{this.state.client.city}</span>
                    }
                    {this.state.action !== 'view' &&
                        <input
                            id="clientCity"
                            type="text"
                            value={this.state.client.city}
                            onChange={(e) => {
                                let nState = this.state;
                                nState.client.city = e.target.value;
                                this.setState(nState);
                            }}
                        />
                    }
                </div>
                <div>
                    <label htmlFor="clientState">UF</label>
                    {this.state.action === 'view' &&
                        <span id="clientState">
                            {this.state.client.state}
                        </span>
                    }
                    {this.state.action !== 'view' &&
                        <select
                            id="clientState"
                            value={this.state.client.state}
                            onChange={(e) => {
                                this.state.client.state = e.target.value;
                                this.setState(this.state);
                            }}
                        >
                            <option key="clientState_AC" value="AC">AC</option>
                            <option key="clientState_AL" value="AL">AL</option>
                            <option key="clientState_AM" value="AM">AM</option>
                            <option key="clientState_AP" value="AP">AP</option>
                            <option key="clientState_BA" value="BA">BA</option>
                            <option key="clientState_CE" value="CE">CE</option>
                            <option key="clientState_DF" value="DF">DF</option>
                            <option key="clientState_ES" value="ES">ES</option>
                            <option key="clientState_GO" value="GO">GO</option>
                            <option key="clientState_MA" value="MA">MA</option>
                            <option key="clientState_MG" value="MG">MG</option>
                            <option key="clientState_MS" value="MS">MS</option>
                            <option key="clientState_MT" value="MT">MT</option>
                            <option key="clientState_PA" value="PA">PA</option>
                            <option key="clientState_PB" value="PB">PB</option>
                            <option key="clientState_PE" value="PE">PE</option>
                            <option key="clientState_PI" value="PI">PI</option>
                            <option key="clientState_PR" value="PR">PR</option>
                            <option key="clientState_RJ" value="RJ">RJ</option>
                            <option key="clientState_RN" value="RN">RN</option>
                            <option key="clientState_RO" value="RO">RO</option>
                            <option key="clientState_RR" value="RR">RR</option>
                            <option key="clientState_RS" value="RS">RS</option>
                            <option key="clientState_SC" value="SC">SC</option>
                            <option key="clientState_SE" value="SE">SE</option>
                            <option key="clientState_SP" value="SP">SP</option>
                            <option key="clientState_TO" value="TO">TO</option>
                        </select>
                    }
                </div>
                <div>
                    <label htmlFor="clientEmail">E-mail</label>
                    {this.state.action === 'view' &&
                        <span id="clientEmail">{this.state.client.email}</span>
                    }
                    {this.state.action !== 'view' &&
                        <input
                            id="clientEmail"
                            type="email"
                            value={this.state.client.email}
                            onChange={(e) => {
                                let nState = this.state;
                                nState.client.email = e.target.value;
                                this.setState(nState);
                            }}
                        />
                    }
                </div>
                <div>
                    <label htmlFor="clientPhone">Telefone</label>
                    {this.state.action === 'view' &&
                        <span id="clientPhone">{this.state.client.phone}</span>
                    }
                    {this.state.action !== 'view' &&
                        <input
                            id="clientPhone"
                            type="tel"
                            value={this.state.client.phone}
                            onChange={(e) => {
                                let nState = this.state;
                                nState.client.phone = e.target.value;
                                this.setState(nState);
                            }}
                        />
                    }
                </div>
                {this.state.action !== 'view' &&
                    <div className="btnBar">
                        <label></label>
                        <button type="submit">Gravar</button>
                    </div>
                }
            </div>
        );
    }

    renderContent() {
        let contractContent = !this.state.contractLoaded
            ? <p><em>Carregando contrato...</em></p>
            : this.renderContract();
        let clientContent = !this.state.clientLoaded
            ? <p><em>Carregando cliente...</em></p>
            : this.renderClient(this.state.client);

        return (
            <div>
                {contractContent}
                {clientContent}
            </div>
            );
    }

    render() {

        let content = !this.state.combosLoaded
            ? <p><em>Carregando...</em></p>
            : this.renderContent();

        return (
            <div>
                <h1 id="tableLabel">Contrato</h1>
                <div id="Contract">
                    <form onSubmit={(e) => { e.preventDefault(); this.handleSubmit(); return false; }} autoComplete="off">
                        {content}
                    </form>
                </div>
            </div>
        );
    }

    async populateCombos() {
        return this.get("contract", "", "types").then(response => {
            if (response.ok) {
                return response.json().then(data => {
                    let nState = this.state;
                    nState.types = data;
                    this.setState(nState);
                });
            }
        }).then(() => {
            return this.get("contract", "", "statuses");
        }).then(response => {
            if (response.ok) {
                return response.json().then(data => {
                    let nState = this.state;
                    nState.statuses = data;
                    nState.combosLoaded = true;
                    this.setState(nState);
                });
            }
        });
    }

    async populate() {
        await this.populateCombos();
        if (this.props.action !== 'new') {
            return this.get('contract', this.state.contractId).then(response => {
                if (response.ok) {
                    return response.json().then(data => {
                        let nState = this.state;
                        nState.contract = data.contract;
                        nState.client = data.client;
                        nState.contractLoaded = true;
                        nState.clientLoaded = true;
                        this.setState(nState);
                    });
                } else {
                    throw new Error();
                }
            }).catch(err => {
                console.error(err);
            });
        }
    }

    searchClient() {
        this.get('client', this.state.client.Document, 'bydoc').then(response => {
            if (response.ok) {
                response.json().then(data => {
                    let newState = this.state;
                    newState.client = data;
                    this.setState(newState);
                });
            } else {
                throw new Error(response);
            }
        }).catch(err => {
            console.error(err.message);
        });
    }

    findAddress() {
        if (this.state.client.zipCode && this.state.client.zipCode.trim().length === 8) {
            let zip = this.state.client.zipCode.trim();
            fetch(`http://viacep.com.br/ws/${zip}/json/`).then(response => {
                if (response.ok) {
                    response.json().then(data => {
                        if (data.erro) {
                            alert('CEP incorreto');
                        } else {
                            let newState = this.state;
                            newState.client.address1 = data.logradouro + ', ' + data.unidade;
                            newState.client.address2 = data.complemento;
                            newState.client.state = data.uf;
                            newState.client.city = data.localidade;

                            if (!newState.client.phone || newState.client.phone.trim().length === 0) {
                                newState.client.phone = data.ddd ? `(${data.ddd}) ` : '';
                            }
                            this.setState(newState);
                        }
                    });
                } else {
                    throw new Error(response);
                }
            }).catch(err => {
                console.error(err.message);
            });
        }
        
    }

    handleSubmit() {
        if (this.state.action === 'new') {
            this.post('contract', { contract: this.state.contract, client: this.state.client }).then(response => {
                if (response.ok) {
                    alert("Contrato criado!");
                    window.location.replace('/');
                } else {
                    alert('Dados informados incorretos. Por favor, revise os campos e tente novamente.');
                }
            }).catch(err => {
                console.error(err);
                alert('Ocorreu um erro. Tente novamente mais tarde.');
            });
        } else if (this.state.action === 'edit') {
            this.put('contract', { contract: this.state.contract, client: this.state.client }).then(response => {
                if (response.ok) {
                    alert("Contrato alterado!");
                    window.location.replace('/');
                } else {
                    alert('Dados informados incorretos. Por favor, revise os campos e tente novamente.');
                }
            }).catch(err => {
                console.error(err);
                alert('Ocorreu um erro. Tente novamente mais tarde.');
            });
        }
    }
    
}