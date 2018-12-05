import { FormGroup } from '@angular/forms';

export class EqualsValidator {

    /**
     * compares 2 values
     * */
    static equals(valueControlNameA: string, valueControlNameB: string) {
        return (group: FormGroup): { [key: string]: any } => {
            // get values
            let valueA = group.get(valueControlNameA).value;
            let valueB = group.get(valueControlNameB).value;

            return valueA === valueB ? null : { equals: true };
        }
    }
}