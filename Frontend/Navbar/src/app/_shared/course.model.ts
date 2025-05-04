export class CourseModel{
    courseId: number;
    name: string;
    description: string;
    assignmentDeadline: Date  ;
    courseUsers:[];
    courseNotes:[];
}
export class CourseModelAssignmentPut{
    courseId: number;
    name: string;
    description: string;
 
}
