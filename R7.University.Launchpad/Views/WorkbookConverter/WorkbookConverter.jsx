class WorkbookConverter extends React.Component {

    constructor (props) {
        super (props);
        this.state = {
            error: { isError: false, errorMessage: "" },
            files: []
        };
    }
    
    renderFile (file) {
        return (
            <tr>
                <td>{file.fileName}</td>
                <td><a href={this.props.service.getUrl ("WorkbookConverter", "Convert", null)
                    + "?fileName=" + encodeURIComponent (file.fileName)
                    + "&tempFilePath=" + encodeURIComponent (file.tempFilePath)
                    + "&format=csv"}>CSV</a></td>
            </tr>
        );
    }
    
    renderFiles (files) {
        if (files.length > 0) {
            return (
                <table className="table table-bordered table-striped table-hover">
                    <thead>
                        <tr>
                            <th>File Name</th>
                            <th>Download</th>
                        </tr>
                    </thead>
                    <tbody>
                        {files.map ((f) => this.renderFile (f))}
                    </tbody>
                </table>
            );
        }
        return null;
    }
    
    renderError (error) {
        return (
            <div className="alert alert-danger">
                <p className="mb-0">{error.errorMessage}</p>
            </div>
        );
    }
    
    render () {
        if (this.state.error.isError === true) {
            return this.renderError (this.state.error);
        }
        return (
            <div>
                <form>
                    <fieldset>
                        <div className="form-group">
                            <label for={"u8y_wbc_upload_" + this.props.moduleId}>Select .XLSX file</label>
                            <input type="file" className="form-control-file" id={"u8y_wbc_upload_" + this.props.moduleId}
                                accept=".xlsx, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                            />
                        </div>    
                        <div className="form-group">    
                            <input type="button" className="btn btn-primary" onClick={this.upload.bind(this)} value="Upload" />
                        </div>
                    </fieldset>
                </form>
                {this.renderFiles (this.state.files)}
            </div>
        );
    }
    
    upload (e) {
        e.preventDefault ();
    
        const formData = new FormData ();
        const file = document.getElementById ("u8y_wbc_upload_" + this.props.moduleId).files [0];
        formData.append ("file", file);
            
        this.props.service.upload (
            formData,    
            (retData) => {
                const newState = {
                    error: { isError: false, errorMessage: "" },
                    files: this.state.files
                };
                newState.files.push ({
                    tempFilePath: retData.tempFilePath,
                    fileName: retData.fileName
                });
                this.setState (newState);
            },
            (xhr, status) => {
                console.log (xhr);
                console.log (status);
                this.setErrorState (xhr.statusText);
            }
        );
    }
    
    setErrorState (errorMessage) {
        this.setState ({
            error: { isError: true, errorMessage: errorMessage },
            files: []
        });
    }
}

(function ($, window, document) {

    class WorkbookConverterService {
        constructor (moduleId) {
            this.moduleId = moduleId;
            this.baseServicepath = $.dnnSF (moduleId).getServiceRoot ("R7.University.Launchpad");
        }
        
        getUrl (controller, action, id) {
            return this.baseServicepath + controller + "/" + action + (id != null ? "/" + id : "");
        }
    
        ajaxCall (type, controller, action, id, data, success, fail) {
            $.ajax ({
                type: type,
                url: this.baseServicepath + controller + "/" + action + (id != null ? "/" + id : ""),
                beforeSend: $.dnnSF (this.moduleId).setModuleHeaders,
                data: data
            }).done (function (retData) {
                if (success != undefined) {
                    success (retData);
                }
            }).fail (function (xhr, status) {
                if (fail != undefined) {
                    fail (xhr, status);
                }
            });
        }
        
        ajaxCall2 (type, controller, action, id, data, success, fail) {
            $.ajax ({
                type: type,
                url: this.baseServicepath + controller + "/" + action + (id != null ? "/" + id : ""),
                beforeSend: $.dnnSF (this.moduleId).setModuleHeaders,
                processData: false,
                contentType: false,
                data: data
            }).done (function (retData) {
                if (success != undefined) {
                    success (retData);
                }
            }).fail (function (xhr, status) {
                if (fail != undefined) {
                    fail (xhr, status);
                }
            });
        }
        
        upload (data, success, fail) {
            this.ajaxCall2 ("POST", "WorkbookConverter", "Upload", null, data, success, fail);
        }
    }

    $(() => {
        $(".u8y-workbook-converter").each ((i, m) => {
            const moduleId = $(m).data ("module-id");
            ReactDOM.render (<WorkbookConverter
                moduleId={moduleId}
                service={new WorkbookConverterService (moduleId)}
            />, m);
        });
    });
}) (jQuery, window, document);