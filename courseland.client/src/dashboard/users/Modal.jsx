import { Component } from 'react';
import './Modal.scss';

export default class Modal extends Component {

    constructor(props) {
        super(props);
    }

    render() {
        const { children, onClose } = this.props

        return (
            <>
                <div className="modal-overlay">
                    <div className="modal">
                        <button className="close-button" onClick={onClose}>×</button>
                        {children}
                    </div>
                </div>
            </>
        );
    }
}