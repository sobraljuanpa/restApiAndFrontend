import { Injectable } from '@angular/core';
import { Adapter } from './adapter';

export class Folder {
    constructor(
        public id: number,
        public name: string
    ){}
}

@Injectable({
    providedIn: 'root'
})
export class FolderAdapter implements Adapter<Folder> {
    adapt(item: any): Folder {
        return new Folder (
            item.Id,
            item.Name
        );
    }
}