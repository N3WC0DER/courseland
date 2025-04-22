import { Component } from 'react';
import { API } from '../../api.jsx';
import './Request.scss';

export default class Request extends Component {

    constructor(props) {
        super(props);

        const { request } = this.props;

        this.state = {
            request: request,

            inputs: [
                { name: "name", type: "text", value: request.name, isLocked: true },
                { name: "location", type: "text", value: request.location, isLocked: true },
                { name: "phone", type: "phone", value: request.phone, isLocked: true },
                { name: "email", type: "email", value: request.email, isLocked: true },
            ]
        }
    }

    handleToggleLock = (id) => {
        this.setState((prevState) => ({
            request: prevState.request,

            inputs: prevState.inputs.map((input, index) =>
                index === id ? { ...input, isLocked: !input.isLocked } : input
            )
        }));
    };

    handleInputChange = (id, event) => {
        this.setState((prevState) => ({
            prevRequest: prevState.prevRequest,

            inputs: prevState.inputs.map((input, index) =>
                index === id ? { ...input, value: event.target.value } : input
            )
        }));
    };

    onSubmit = (req) => {

        const { inputs } = this.state;

        const request = {
            id: req.id,
            status: req.status,
            name: inputs.find(input => input.name === "name").value,
            location: inputs.find(input => input.name === "location").value,
            phone: inputs.find(input => input.name === "phone").value,
            email: inputs.find(input => input.name === "email").value,
            datetime: req.dateTime + "Z",
            linkEndpoint: req.linkEndpoint
        };

        let xhr = new XMLHttpRequest();
        xhr.open("put", API.registration_request + "/" + request.id, true);
        xhr.setRequestHeader("Content-Type", "application/json");
        xhr.onload = function () {
            console.debug("done.");
            window.location.reload();
        }

        xhr.onerror = function () {
            console.debug("server error");
        }

        xhr.send(JSON.stringify(request));
    }


    render() {
        const { request, inputs } = this.state;

        return (
            <>
                <div id="request-modal">
                    <h2>Обработка запроса #{request.id}</h2>
                    <form action={this.onSubmit.bind(this, request)} autoComplete="off">
                        {
                            inputs.map((value, id) => (
                                <label key={id}>
                                    {value.name}:
                                    <div className="row">
                                        <input
                                            name={value.name}
                                            type={value.type}
                                            id={value.name}
                                            value={value.value}
                                            disabled={value.isLocked}
                                            onChange={(e) => this.handleInputChange(id, e)}
                                        />
                                        <button
                                            type="button"
                                            onClick={() => this.handleToggleLock(id)}
                                        >
                                            {value.isLocked ? 'Edit' : 'Save'}
                                        </button>
                                    </div>
                                </label>
                            ))
                        }
                        <span id="datetime">{new Date(request.dateTime + "Z").toLocaleString("ru")}</span>
                        <button type="submit">
                            Submit
                        </button>
                    </form>
                </div>
            </>
        );
    }
}