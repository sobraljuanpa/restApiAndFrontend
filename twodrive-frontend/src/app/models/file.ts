import { Injectable } from '@angular/core';
import { Adapter } from './adapter';

export class File {
    constructor(
        public id: number,
        public name: string,
        public content: string
    ){}
}

@Injectable({
    providedIn: 'root'
})
export class FileAdapter implements Adapter<File> {
    adapt(item: any): File {
        return new File (
            item.Id,
            item.Name,
            item.Content
        );
    }
}