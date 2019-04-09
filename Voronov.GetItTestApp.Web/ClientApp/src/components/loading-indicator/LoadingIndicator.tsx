import React from 'react';

const LoadingIndicator = (props: any) => {
    return (
        <div className="d-flex justify-content-center">
            <div className="spinner-border" role="status">
                <span className="sr-only">Loading...</span>
            </div>
        </div>
    );
};
export default LoadingIndicator;