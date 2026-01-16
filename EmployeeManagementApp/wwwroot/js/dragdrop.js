// Drag and drop functionality for employee management
let draggedEmployeeId = null;

window.employeeDragDrop = {
    // Called when dragging starts
    onDragStart: function (event, employeeId) {
        draggedEmployeeId = employeeId;
        event.dataTransfer.setData("text/plain", employeeId);
        // Add visual feedback
        const element = event.target;
        element.classList.add('dragging');
    },

    // Called when dragging ends
    onDragEnd: function (event) {
        const element = event.target;
        element.classList.remove('dragging');
        draggedEmployeeId = null;
    },

    // Called when something is dragged over a drop zone
    onDragOver: function (event) {
        event.preventDefault(); // Necessary to allow dropping
        const dropZone = event.currentTarget;
        dropZone.classList.add('drop-zone-hover');
    },

    // Called when something is dragged out of a drop zone
    onDragLeave: function (event) {
        const dropZone = event.currentTarget;
        dropZone.classList.remove('drop-zone-hover');
    },

    // Called when something is dropped onto a drop zone
    onDrop: function (event, position) {
        event.preventDefault();
        const dropZone = event.currentTarget;
        dropZone.classList.remove('drop-zone-hover');

        const employeeId = event.dataTransfer.getData("text/plain");
        if (employeeId) {
            // Return the employee ID and position to Blazor
            return { employeeId: parseInt(employeeId), position: position };
        }
        return null;
    }
};