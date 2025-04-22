import { Component } from 'react';
import Modal from './Modal.jsx';
import { API } from '../../api.jsx';
import './Lists.scss';
import Request from './Request.jsx';

export default class RequestsList extends Component {

    constructor(props) {
        super(props);

        const requests = [];

        this.state = {
            selected: null,
            requests: requests,
            loading: true
        }
    }

    componentDidMount() {
        // todo: change to fetch

        let xhr = new XMLHttpRequest();
        xhr.open("get", API.registration_request, true);
        xhr.setRequestHeader("Content-Type", "application/json");
        xhr.onload = function () {
            if (xhr.status >= 200 && xhr.status < 300) {
                let requests = JSON.parse(xhr.response);
                console.debug("Requests loaded: " + requests.length);
                this.setState({ selected: null, requests: requests, loading: false});
            } else {
                this.setState({ error: `Ошибка: ${xhr.statusText}`, loading: false });
            }
        }.bind(this);

        xhr.onerror = () => {
            console.debug("server error");
            this.setState({ error: 'Ошибка при выполнении запроса', loading: false });
        };

        xhr.send();
    }

    setSelectedRequest = (request) => {
        const { selected } = this.state;

        if (selected !== null) {
            fetch(API.add_id_and_param(API.registration_request, selected.id, { status: 0 }), {
                method: 'PATCH'
            })
                .then(response => response.json())
                .then(result => console.debug(result));
        }

        if (request === null) {
            this.setState((prevState) => ({
                selected: null,
                requests: prevState.requests,
                loading: false
            }));
        }

        this.setState((prevState) => ({
            selected: null,
            requests: prevState.requests,
            loading: true
        }));



        this.setState((prevState) => ({
            selected: request,
            requests: prevState.requests,
            loading: false
        }));
    }

    render() {

        const { selected, requests, loading, error } = this.state;
        const { searchQuery } = this.props;

        const filteredRequests = error ?
            null :
            requests.filter(request => request.name.toLowerCase().includes(searchQuery.toLowerCase()));

        let requestsList;

        if (filteredRequests.length == 0 || loading || error) {
            requestsList = "Пусто";
        } else {
            requestsList = filteredRequests.map(request => (
                <div key={request.id} className="request-item">
                    <span className="id">{request.id}</span>
                    <span className="name">{request.name}</span>
                    <span className="date">{new Date(request.dateTime + "Z").toLocaleString("ru")}</span>
                    <button onClick={this.setSelectedRequest.bind(this, request)}>Обработать</button>
                </div>
            ));
        }

        return (
            <>
                <div id="requests-list">
                    { requestsList }
                    {selected && (
                        <Modal onClose={this.setSelectedRequest.bind(this, null)}>
                            <Request request={selected} />
                        </Modal>
                    )}
                </div>
            </>
        );
    }
}