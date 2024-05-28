import React from 'react'

const Swagger = () => {
    return (
        <div style={{ width: '100%', height: '100vh' }}>
            <iframe
                src='https://localhost:7255/swagger/index.html'
                style={{ width: '100%', height: '100%', border: 'none' }}
                title= "API Documentation By Swagger"
            />
        </div>
    )
}

export default Swagger;