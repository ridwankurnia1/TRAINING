import { Dropdown } from './Dropdown';

export interface LebaranQuiz {
    id?: number;
    pertanyaan?: string;
    category?: string;
    nilai?: string;
    options?: Dropdown[];
    invalid?: boolean;
}
