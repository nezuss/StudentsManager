import { useState } from 'react';
import axios from 'axios';

function App() {
    const [id, setId] = useState("");
    const [name, setName] = useState("");
    const [age, setAge] = useState("");

    const CSTid = (event) => { setId(event.target.value); };
    const CSTname = (event) => { setName(event.target.value); };
    const CSTage = (event) => { setAge(event.target.value); };

    async function GetStudents() {
        const responseContent = document.getElementById("response");
        const link = "http://localhost:5116/api/Students/get";
        
        try {
            const response = await axios.get(link);
            
            responseContent.innerHTML = JSON.stringify(response.data, null, 2);
        } catch (error) {
            if (error.response) {
                responseContent.innerHTML = `Error: ${error.response.status} - ${JSON.stringify(error.response.data, null, 2)}`;
            } else {
                responseContent.innerHTML = "Error: " + error.message;
            }
        }
    }

    async function CreateStudent() {
        const responseContent = document.getElementById("response");
        const link = "http://localhost:5116/api/Students/create";

        try {
            const response = await axios.post(link+'/'+name+'/'+age);
            
            responseContent.innerHTML = JSON.stringify(response.data, null, 2);
        } catch (error) {
            if (error.response) {
                responseContent.innerHTML = `Error: ${error.response.status} - ${JSON.stringify(error.response.data, null, 2)}`;
            } else {
                responseContent.innerHTML = "Error: " + error.message;
            }
        }
    }

    async function UpdateStudent() {
        const responseContent = document.getElementById("response");
        const link = "http://localhost:5116/api/Students/update";

        try {
            const response = await axios.patch(link, {
                id: id,
                name: name,
                age: age
            });
            
            responseContent.innerHTML = JSON.stringify(response.data, null, 2);
        } catch (error) {
            if (error.response) {
                responseContent.innerHTML = `Error: ${error.response.status} - ${JSON.stringify(error.response.data, null, 2)}`;
            } else {
                responseContent.innerHTML = "Error: " + error.message;
            }
        }
    }

    async function DeleteStudent() {
        const responseContent = document.getElementById("response");
        const link = "http://localhost:5116/api/Students/delete";

        try {
            const response = await axios.delete(link+'/'+id);
            
            responseContent.innerHTML = JSON.stringify(response.data, null, 2);
        } catch (error) {
            if (error.response) {
                responseContent.innerHTML = `Error: ${error.response.status} - ${JSON.stringify(error.response.data, null, 2)}`;
            } else {
                responseContent.innerHTML = "Error: " + error.message;
            }
        }
    }

    return (
        <div className="flex row align-start justify-center gap-12 p-12">
            <div className="flex column justify-center align-center gap-12 w-350" style={{paddingLeft: "13px", paddingRight: "13px"}}>
                <div className="flex gap-12 bg-2 b w-full p-12 br-12">
                    <div className="flex column w-full">
                        <h2>Get students</h2>
                        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris et.</p>
                        <br />
                        <button onClick={GetStudents} type="submit" className="p-8 br-8">Get</button>
                    </div>
                </div>
                <div className="flex gap-12 bg-2 b w-full p-12 br-12">
                    <div className="flex column w-full">
                        <h2>Create student</h2>
                        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed tristique.</p>
                        <br />
                        <div className="flex column gap-12">
                            <div className="flex column">
                                <label htmlFor="csf-name">Student name</label>
                                <input id="csf-name" onChange={CSTname} type="text" className="p-8 br-8" />
                            </div>
                            <div className="flex column">
                                <label htmlFor="csf-age">Student age</label>
                                <input id="csf-age" onChange={CSTage} type="text" className="p-8 br-8" />
                            </div>
                        </div>
                        <br />
                        <button onClick={CreateStudent} type="submit" className="p-8 br-8">Create</button>
                    </div>
                </div>
                <div className="flex gap-12 bg-2 b w-full p-12 br-12">
                    <div className="flex column w-full">
                        <h2>Edit student</h2>
                        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec ut.</p>
                        <br />
                        <div className="flex column gap-12">
                            <div className="flex column">
                                <label htmlFor="esf-id">Student id</label>
                                <input id="esf-id" onChange={CSTid} type="text" className="p-8 br-8" />
                            </div>
                            <div className="flex column">
                                <label htmlFor="esf-name">Student name</label>
                                <input id="esf-name" onChange={CSTname} type="text" className="p-8 br-8" />
                            </div>
                            <div className="flex column">
                                <label htmlFor="esf-age">Student age</label>
                                <input id="esf-age" onChange={CSTage} type="text" className="p-8 br-8" />
                            </div>
                        </div>
                        <br />
                        <button onClick={UpdateStudent} type="submit" className="p-8 br-8">Save</button>
                    </div>
                </div>
                <div className="flex gap-12 bg-2 b w-full p-12 br-12">
                    <div className="flex column w-full">
                        <h2>Delete student</h2>
                        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras porttitor.</p>
                        <br />
                        <label htmlFor="dsf-id">Student id</label>
                        <input id="dsf-id" onChange={CSTid} type="text" className="p-8 br-8" />
                        <br />
                        <button onClick={DeleteStudent} type="submit" className="p-8 br-8">Delete</button>
                    </div>
                </div>
            </div>
            <div className="flex column gap-12 max-w-600 w-600 bg-2 b p-12 br-12">
                <h2 className="c-2">Response</h2>
                <hr />
                <pre id="response"></pre>
            </div>
        </div>
    );
}

export default App;