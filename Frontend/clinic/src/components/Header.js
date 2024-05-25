import React from 'react';
import { Link } from 'react-router-dom';

const Header = () => {
    return (
        <header>
            <nav>
                <ul>
                    <li><Link to ='/book'>Book Appointment</Link></li>
                    <li><Link to ='/search'>Search Doctor</Link></li>
                </ul>
            </nav>
        </header>
    )
}

export default Header;