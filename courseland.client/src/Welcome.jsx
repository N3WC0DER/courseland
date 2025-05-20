import { Component } from 'react';
import './Welcome.scss';
import { API } from './api.jsx';

export default class Welcome extends Component {

    constructor(props) {
        super(props);

        this.state = { counter: 0 };

    }

    onSubmit(formData) {
        let data = {
            name: formData.get("name"),
            location: formData.get("location"),
            phone: formData.get("phone"),
            email: formData.get("email"),
            datetime: new Date
        }

        let xhr = new XMLHttpRequest();
        xhr.open("post", API.registration_request, true);
        xhr.setRequestHeader("Content-Type", "application/json");
        xhr.onload = function () {
            console.debug("done.");
        }

        xhr.onerror = function () {
            console.debug("server error");
        }

        xhr.send(JSON.stringify(data));
    }

    render() {
        return (
            <>
                <form action={this.onSubmit} id="registration">
                    <input name="name" placeholder="name"></input>
                    <input name="location" placeholder="location"></input>
                    <input name="phone" placeholder="phone"></input>
                    <input name="email" placeholder="email"></input>
                    <input type="submit" placeholder="Submit"></input>
                </form>
            </>
        );
    }   
}